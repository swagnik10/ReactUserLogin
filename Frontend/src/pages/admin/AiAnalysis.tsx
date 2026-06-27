import { useEffect, useState } from "react";
import {
  compareRoles,
  getRoles,
} from "../../services/role/roleService";
import type {
  RoleComparisonDto,
  RoleSummaryDto,
} from "../../features/auth/types/role";
import { toast } from "sonner";
import RoleComparisonForm from "../../components/ai-analysis/RoleComparisonForm";
import ComparisonSummary from "../../components/ai-analysis/ComparisonSummary";
import ComparisonSection from "../../components/ai-analysis/ComparisonSection";

const AiAnalysis = () => {
  const [roles, setRoles] = useState<RoleSummaryDto[]>([]);

  const [roleA, setRoleA] = useState("");
  const [roleB, setRoleB] = useState("");

  const [comparison, setComparison] =
    useState<RoleComparisonDto | null>(null);

  const [loadingComparison, setLoadingComparison] =
    useState(false);

  useEffect(() => {
    loadRoles();
  }, []);

  const loadRoles = async () => {
    try {
      const data = await getRoles();

      setRoles(data);

      if (data.length >= 2) {
        setRoleA(data[0].name);
        setRoleB(data[1].name);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const handleCompare = async () => {
    if (!roleA || !roleB)
      return;

    if (roleA === roleB) {
      toast.error("Please select two different roles.");
      return;
    }

    try {
      setLoadingComparison(true);

      const result = await compareRoles(
        roleA,
        roleB
      );

      setComparison(result);
    } catch (error) {
      toast.error("Failed to compare roles.");
    } finally {
      setLoadingComparison(false);
    }
  };

  const handleRoleAChange = (value: string) => {
    setRoleA(value);
    setComparison(null);
  };

  const handleRoleBChange = (value: string) => {
    setRoleB(value);
    setComparison(null);
  };

  return (
    <div className="space-y-6">

      <h1 className="text-3xl font-bold">
        AI Analysis
      </h1>

      <RoleComparisonForm
        roles={roles}
        roleA={roleA}
        roleB={roleB}
        loading={loadingComparison}
        onRoleAChange={handleRoleAChange}
        onRoleBChange={handleRoleBChange}
        onCompare={handleCompare}
      />

      {comparison && (
        <div className="space-y-6">

          <ComparisonSummary
            summary={comparison.summary}
          />

          <ComparisonSection
            title="Shared Permissions"
            items={comparison.similarities}
          />

          <ComparisonSection
            title="Differences"
            items={comparison.differences}
          />

          <ComparisonSection
            title={`Only ${roleA}`}
            items={comparison.permissionsOnlyInRoleA}
          />

          <ComparisonSection
            title={`Only ${roleB}`}
            items={comparison.permissionsOnlyInRoleB}
          />

          <ComparisonSection
            title="Recommended Usage"
            items={comparison.recommendedUseCases}
          />

          <ComparisonSection
            title="Security Implications"
            items={comparison.securityImplications}
          />

        </div>
      )}

    </div>
  );
};



export default AiAnalysis;