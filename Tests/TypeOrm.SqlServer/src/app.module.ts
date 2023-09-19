import { Module, Logger } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { UsersModules } from './users/users.modules';
import { BasicAuditingSubscriber } from './typeorm/basic-auditing-subscriber';
import { typeOrmConfig } from './orm.config';
import { TypeOrmExModule } from './typeorm/typeorm-ex.module';
import { A_RequiredCompositeRepository } from './repository/a-required-composite.repository';
import { B_OptionalAggregateRepository } from './repository/b-optional-aggregate.repository';
import { B_OptionalDependentRepository } from './repository/b-optional-dependent.repository';
import { C_MultipleDependentRepository } from './repository/c-multiple-dependent.repository';
import { ComplexDefaultIndexRepository } from './repository/complex-default-index.repository';
import { CustomIndexRepository } from './repository/custom-index.repository';
import { D_MultipleDependentRepository } from './repository/d-multiple-dependent.repository';
import { D_OptionalAggregateRepository } from './repository/d-optional-aggregate.repository';
import { DefaultIndexRepository } from './repository/default-index.repository';
import { E_RequiredCompositeNavRepository } from './repository/e-required-composite-nav.repository';
import { F_OptionalAggregateNavRepository } from './repository/f-optional-aggregate-nav.repository';
import { F_OptionalDependentRepository } from './repository/f-optional-dependent.repository';
import { FK_A_CompositeForeignKeyRepository } from './repository/fk-a-composite-foreign-key.repository';
import { G_RequiredCompositeNavRepository } from './repository/g-required-composite-nav.repository';
import { H_MultipleDependentRepository } from './repository/h-multiple-dependent.repository';
import { H_OptionalAggregateNavRepository } from './repository/h-optional-aggregate-nav.repository';
import { InhabitantRepository } from './repository/inhabitant.repository';
import { J_MultipleAggregateRepository } from './repository/j-multiple-aggregate.repository';
import { J_RequiredDependentRepository } from './repository/j-required-dependent.repository';
import { K_SelfReferenceRepository } from './repository/k-self-reference.repository';
import { L_SelfReferenceMultipleRepository } from './repository/l-self-reference-multiple.repository';
import { M_SelfReferenceBiNavRepository } from './repository/m-self-reference-bi-nav.repository';
import { PK_A_CompositeKeyRepository } from './repository/pk-a-composite-key.repository';
import { PK_PrimaryKeyIntRepository } from './repository/pk-primary-key-int.repository';
import { PK_PrimaryKeyLongRepository } from './repository/pk-primary-key-long.repository';
import { StereotypeIndexRepository } from './repository/stereotype-index.repository';
import { TextureRepository } from './repository/texture.repository';
import { TpcConcreteBaseClassRepository } from './repository/tpc-concrete-base-class.repository';
import { TpcConcreteBaseClassAssociatedRepository } from './repository/tpc-concrete-base-class-associated.repository';
import { TpcDerivedClassForAbstractRepository } from './repository/tpc-derived-class-for-abstract.repository';
import { TpcDerivedClassForAbstractAssociatedRepository } from './repository/tpc-derived-class-for-abstract-associated.repository';
import { TpcDerivedClassForConcreteRepository } from './repository/tpc-derived-class-for-concrete.repository';
import { TpcDerivedClassForConcreteAssociatedRepository } from './repository/tpc-derived-class-for-concrete-associated.repository';
import { TpcFkAssociatedClassRepository } from './repository/tpc-fk-associated-class.repository';
import { TpcFkBaseClassRepository } from './repository/tpc-fk-base-class.repository';
import { TpcFkBaseClassAssociatedRepository } from './repository/tpc-fk-base-class-associated.repository';
import { TpcFkDerivedClassRepository } from './repository/tpc-fk-derived-class.repository';
import { TpcPoly_BaseClassNonAbstractRepository } from './repository/tpc-poly-base-class-non-abstract.repository';
import { TpcPoly_ConcreteARepository } from './repository/tpc-poly-concrete-a.repository';
import { TpcPoly_ConcreteBRepository } from './repository/tpc-poly-concrete-b.repository';
import { TpcPoly_RootAbstract_AggrRepository } from './repository/tpc-poly-root-abstract-aggr.repository';
import { TpcPoly_SecondLevelRepository } from './repository/tpc-poly-second-level.repository';
import { TpcPoly_TopLevelRepository } from './repository/tpc-poly-top-level.repository';
import { TphAbstractBaseClassRepository } from './repository/tph-abstract-base-class.repository';
import { TphAbstractBaseClassAssociatedRepository } from './repository/tph-abstract-base-class-associated.repository';
import { TphConcreteBaseClassRepository } from './repository/tph-concrete-base-class.repository';
import { TphConcreteBaseClassAssociatedRepository } from './repository/tph-concrete-base-class-associated.repository';
import { TphDerivedClassForAbstractRepository } from './repository/tph-derived-class-for-abstract.repository';
import { TphDerivedClassForAbstractAssociatedRepository } from './repository/tph-derived-class-for-abstract-associated.repository';
import { TphDerivedClassForConcreteRepository } from './repository/tph-derived-class-for-concrete.repository';
import { TphDerivedClassForConcreteAssociatedRepository } from './repository/tph-derived-class-for-concrete-associated.repository';
import { TphFkAssociatedClassRepository } from './repository/tph-fk-associated-class.repository';
import { TphFkBaseClassRepository } from './repository/tph-fk-base-class.repository';
import { TphFkBaseClassAssociatedRepository } from './repository/tph-fk-base-class-associated.repository';
import { TphFkDerivedClassRepository } from './repository/tph-fk-derived-class.repository';
import { TphPoly_BaseClassNonAbstractRepository } from './repository/tph-poly-base-class-non-abstract.repository';
import { TphPoly_ConcreteARepository } from './repository/tph-poly-concrete-a.repository';
import { TphPoly_ConcreteBRepository } from './repository/tph-poly-concrete-b.repository';
import { TphPoly_RootAbstractRepository } from './repository/tph-poly-root-abstract.repository';
import { TphPoly_RootAbstract_AggrRepository } from './repository/tph-poly-root-abstract-aggr.repository';
import { TphPoly_SecondLevelRepository } from './repository/tph-poly-second-level.repository';
import { TphPoly_TopLevelRepository } from './repository/tph-poly-top-level.repository';
import { TptAbstractBaseClassRepository } from './repository/tpt-abstract-base-class.repository';
import { TptAbstractBaseClassAssociatedRepository } from './repository/tpt-abstract-base-class-associated.repository';
import { TptConcreteBaseClassRepository } from './repository/tpt-concrete-base-class.repository';
import { TptConcreteBaseClassAssociatedRepository } from './repository/tpt-concrete-base-class-associated.repository';
import { TptDerivedClassForAbstractRepository } from './repository/tpt-derived-class-for-abstract.repository';
import { TptDerivedClassForAbstractAssociatedRepository } from './repository/tpt-derived-class-for-abstract-associated.repository';
import { TptDerivedClassForConcreteRepository } from './repository/tpt-derived-class-for-concrete.repository';
import { TptDerivedClassForConcreteAssociatedRepository } from './repository/tpt-derived-class-for-concrete-associated.repository';
import { TptFkAssociatedClassRepository } from './repository/tpt-fk-associated-class.repository';
import { TptFkBaseClassRepository } from './repository/tpt-fk-base-class.repository';
import { TptFkBaseClassAssociatedRepository } from './repository/tpt-fk-base-class-associated.repository';
import { TptFkDerivedClassRepository } from './repository/tpt-fk-derived-class.repository';
import { TptPoly_BaseClassNonAbstractRepository } from './repository/tpt-poly-base-class-non-abstract.repository';
import { TptPoly_ConcreteARepository } from './repository/tpt-poly-concrete-a.repository';
import { TptPoly_ConcreteBRepository } from './repository/tpt-poly-concrete-b.repository';
import { TptPoly_RootAbstractRepository } from './repository/tpt-poly-root-abstract.repository';
import { TptPoly_RootAbstract_AggrRepository } from './repository/tpt-poly-root-abstract-aggr.repository';
import { TptPoly_SecondLevelRepository } from './repository/tpt-poly-second-level.repository';
import { TptPoly_TopLevelRepository } from './repository/tpt-poly-top-level.repository';
import { TreeRepository } from './repository/tree.repository';
import { ConfigModule } from '@nestjs/config';
import { ClsModule } from 'nestjs-cls';
import { TypeOrmModule } from '@nestjs/typeorm';
import { IntentMerge } from './intent/intent.decorators';

@IntentMerge()
@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    AuthModule,
    UsersModules,
    ClsModule.forRoot({
      global: true,
      middleware: { mount: true },
    }),
    TypeOrmModule.forRoot(typeOrmConfig),
    TypeOrmExModule.forCustomRepository([
      A_RequiredCompositeRepository,
      B_OptionalAggregateRepository,
      B_OptionalDependentRepository,
      C_MultipleDependentRepository,
      ComplexDefaultIndexRepository,
      CustomIndexRepository,
      D_MultipleDependentRepository,
      D_OptionalAggregateRepository,
      DefaultIndexRepository,
      E_RequiredCompositeNavRepository,
      F_OptionalAggregateNavRepository,
      F_OptionalDependentRepository,
      FK_A_CompositeForeignKeyRepository,
      G_RequiredCompositeNavRepository,
      H_MultipleDependentRepository,
      H_OptionalAggregateNavRepository,
      InhabitantRepository,
      J_MultipleAggregateRepository,
      J_RequiredDependentRepository,
      K_SelfReferenceRepository,
      L_SelfReferenceMultipleRepository,
      M_SelfReferenceBiNavRepository,
      PK_A_CompositeKeyRepository,
      PK_PrimaryKeyIntRepository,
      PK_PrimaryKeyLongRepository,
      StereotypeIndexRepository,
      TextureRepository,
      TpcConcreteBaseClassRepository,
      TpcConcreteBaseClassAssociatedRepository,
      TpcDerivedClassForAbstractRepository,
      TpcDerivedClassForAbstractAssociatedRepository,
      TpcDerivedClassForConcreteRepository,
      TpcDerivedClassForConcreteAssociatedRepository,
      TpcFkAssociatedClassRepository,
      TpcFkBaseClassRepository,
      TpcFkBaseClassAssociatedRepository,
      TpcFkDerivedClassRepository,
      TpcPoly_BaseClassNonAbstractRepository,
      TpcPoly_ConcreteARepository,
      TpcPoly_ConcreteBRepository,
      TpcPoly_RootAbstract_AggrRepository,
      TpcPoly_SecondLevelRepository,
      TpcPoly_TopLevelRepository,
      TphAbstractBaseClassRepository,
      TphAbstractBaseClassAssociatedRepository,
      TphConcreteBaseClassRepository,
      TphConcreteBaseClassAssociatedRepository,
      TphDerivedClassForAbstractRepository,
      TphDerivedClassForAbstractAssociatedRepository,
      TphDerivedClassForConcreteRepository,
      TphDerivedClassForConcreteAssociatedRepository,
      TphFkAssociatedClassRepository,
      TphFkBaseClassRepository,
      TphFkBaseClassAssociatedRepository,
      TphFkDerivedClassRepository,
      TphPoly_BaseClassNonAbstractRepository,
      TphPoly_ConcreteARepository,
      TphPoly_ConcreteBRepository,
      TphPoly_RootAbstractRepository,
      TphPoly_RootAbstract_AggrRepository,
      TphPoly_SecondLevelRepository,
      TphPoly_TopLevelRepository,
      TptAbstractBaseClassRepository,
      TptAbstractBaseClassAssociatedRepository,
      TptConcreteBaseClassRepository,
      TptConcreteBaseClassAssociatedRepository,
      TptDerivedClassForAbstractRepository,
      TptDerivedClassForAbstractAssociatedRepository,
      TptDerivedClassForConcreteRepository,
      TptDerivedClassForConcreteAssociatedRepository,
      TptFkAssociatedClassRepository,
      TptFkBaseClassRepository,
      TptFkBaseClassAssociatedRepository,
      TptFkDerivedClassRepository,
      TptPoly_BaseClassNonAbstractRepository,
      TptPoly_ConcreteARepository,
      TptPoly_ConcreteBRepository,
      TptPoly_RootAbstractRepository,
      TptPoly_RootAbstract_AggrRepository,
      TptPoly_SecondLevelRepository,
      TptPoly_TopLevelRepository,
      TreeRepository,
    ])
  ],
  controllers: [],
  providers: [
    Logger,
    BasicAuditingSubscriber
  ]
})
export class AppModule { }