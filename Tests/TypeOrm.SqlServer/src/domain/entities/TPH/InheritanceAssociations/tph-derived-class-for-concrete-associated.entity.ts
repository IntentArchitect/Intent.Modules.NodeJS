import { TphDerivedClassForConcrete } from './tph-derived-class-for-concrete.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_derived_class_for_concrete_associated')
export class TphDerivedClassForConcreteAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  derivedClassForConcreteId: string;

  @ManyToOne(() => TphDerivedClassForConcrete, { cascade: ['insert', 'update'], nullable: false })
  derivedClassForConcrete: TphDerivedClassForConcrete;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}